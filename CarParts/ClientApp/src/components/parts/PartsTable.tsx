import * as React from "react";
import { alpha } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import DeleteIcon from "@mui/icons-material/Delete";
import { visuallyHidden } from "@mui/utils";
import { Button, Pagination, TextField } from "@mui/material";
import { Receipt, Search } from "@mui/icons-material";
import { PartItem } from "@/api/Part/GetAllDto";
import { useQuery, useQueryClient } from "react-query";
import { BrandItem } from "@/api/Brand/dto";
import { GetAllCar } from "@/api/Car/dto";
import { SERVER_URL } from "@/../app.config";

type Order = "asc" | "desc";

interface HeadCell {
  id: keyof PartItem;
  label: string;
  numeric: boolean;
}

const headCells: readonly HeadCell[] = [
  {
    id: "name",
    numeric: false,
    label: "القطعة",
  },
  {
    id: "cars",
    numeric: true,
    label: "السيارات",
  },
  {
    id: "code",
    numeric: false,
    label: "رمز القطعة",
  },

  {
    id: "orginalPrice",
    numeric: true,
    label: "السعر الأصلي",
  },
  {
    id: "sellingPrice",
    numeric: true,
    label: "سعر المبيع",
  },
  {
    id: "brandId",
    numeric: true,
    label: "العلامة التجارية",
  },
];

interface EnhancedTableProps {
  numSelected: number;
  onRequestSort: (
    event: React.MouseEvent<unknown>,
    property: keyof PartItem
  ) => void;
  onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
  order: Order;
  orderBy: string;
  rowCount: number;
}

function EnhancedTableHead(props: EnhancedTableProps) {
  const {
    onSelectAllClick,
    order,
    orderBy,
    numSelected,
    rowCount,
    onRequestSort,
  } = props;
  const createSortHandler =
    (property: keyof PartItem) => (event: React.MouseEvent<unknown>) => {
      onRequestSort(event, property);
    };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            sortDirection={orderBy === headCell.id ? order : false}
          >
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : "asc"}
              onClick={createSortHandler(headCell.id)}
            >
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}

interface EnhancedTableToolbarProps {
  numSelected: number;
  onGenerateInvoice: () => void;
}

function EnhancedTableToolbar(props: EnhancedTableToolbarProps) {
  const { numSelected, onGenerateInvoice } = props;

  return (
    <Toolbar
      sx={{
        px: 2,
        ...(numSelected > 0 && {
          bgcolor: (theme) =>
            alpha(
              theme.palette.primary.main,
              theme.palette.action.activatedOpacity
            ),
        }),
      }}
    >
      {numSelected > 0 ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} قطع تم اختيارها
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 25%" }}
          variant="h6"
          id="tableTitle"
          fontSize={18}
          component="div"
        >
          جميع القطع
        </Typography>
      )}
      {numSelected > 0 ? (
        <Box gap={4} display={"flex"} alignItems={"center"}>
          <Button
            className="flex-grow whitespace-nowrap"
            variant="contained"
            size="large"
            onClick={() => onGenerateInvoice()}
            startIcon={<Receipt />}
          >
            توليد فاتورة
          </Button>
          <Tooltip title="Delete">
            <IconButton>
              <DeleteIcon />
            </IconButton>
          </Tooltip>
        </Box>
      ) : (
        <Box>
          <Box display={"flex"} flexWrap="wrap" gap={2} padding="10px 0">
            <TextField
              variant="standard"
              sx={{ maxWidth: "300px" , width:"auto" }}
              label="البحث عن قطعة معينة"
              InputProps={{
                endAdornment: <Search></Search>,
              }}
            />
          </Box>
        </Box>
      )}
    </Toolbar>
  );
}

export default function PartsTable({
  rows,
  totalCount,
  onPageChange,
  rowsPerPage,
  page,
  onGenerateInvoice,
}: {
  rows: PartItem[];
  totalCount: number;
  onPageChange: (newPage: number, rowsPerPage: number) => void;
  onGenerateInvoice: (parts: PartItem[]) => void;
  rowsPerPage: number;
  page: number;
}) {
  const queryClient = useQueryClient();
  const brands = queryClient.getQueryData<BrandItem[]>("brands");
  const cars = queryClient.getQueryData<GetAllCar[]>("car");
  const [selected, setSelected] = React.useState<readonly number[]>([]);
  const [dense, setDense] = React.useState(false);
  const [order, setOrder] = React.useState<Order>("asc");
  const [orderBy, setOrderBy] = React.useState<keyof PartItem>("createdAt");

  const getBrandName = (id: number) => brands?.find((b) => b.id === id)?.name;
  const handleRequestSort = (
    event: React.MouseEvent<unknown>,
    property: keyof PartItem
  ) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      const newSelected = rows.map((n) => n.id);
      setSelected(newSelected);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
    const selectedIndex = selected.indexOf(id);
    let newSelected: readonly number[] = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }

    setSelected(newSelected);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    onPageChange(newPage, rowsPerPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    onPageChange(page, parseInt(event.target.value, 10));
  };

  const isSelected = (id: number) => selected.indexOf(id) !== -1;

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  return (
    <Box sx={{ width: "100%" }}>
      <Paper sx={{ width: "100%", mb: 2 }} >
        <EnhancedTableToolbar
          onGenerateInvoice={() =>
            onGenerateInvoice(rows.filter((item) => selected.includes(item.id)))
          }
          numSelected={selected.length}
        />
        <TableContainer>
          <Table
            sx={{ minWidth: 750 }}
            aria-labelledby="tableTitle"
            size={dense ? "small" : "medium"}
          >
            <EnhancedTableHead
              numSelected={selected.length}
              order={order}
              orderBy={orderBy}
              onSelectAllClick={handleSelectAllClick}
              onRequestSort={handleRequestSort}
              rowCount={rows.length}
            />
            <TableBody>
              {rows.map((row, index) => {
                const isItemSelected = isSelected(row.id);

                const labelId = `enhanced-table-checkbox-${index}`;

                return (
                  <TableRow
                    hover
                    sx={{ paddingX: 10 }}
                    onClick={(event) => handleClick(event, row.id)}
                    role="checkbox"
                    aria-checked={isItemSelected}
                    tabIndex={-1}
                    key={row.id}
                    selected={isItemSelected}
                  >
                    <TableCell component="th" id={labelId} scope="row">
                      <div className="flex items-center gap-8">
                        <img
                          alt={row.name}
                          height={65}
                          width={65}
                          className="transition duration-150 hover:scale-150"
                          src={`${SERVER_URL}/${row.image}`}
                        />
                        <span>{row.name}</span>
                      </div>
                    </TableCell>
                    <TableCell>
                      {cars
                        ?.filter((c) => row.cars.includes(c.id))
                        .map((c) => c.name)
                        .join(" , ")}
                    </TableCell>
                    <TableCell component="th" id={labelId} scope="row">
                      {row.code}
                    </TableCell>
                    <TableCell>{row.orginalPrice}</TableCell>
                    <TableCell>{row.sellingPrice}</TableCell>
                    <TableCell>{getBrandName(row.brandId)}</TableCell>
                  </TableRow>
                );
              })}
              {emptyRows > 0 && (
                <TableRow>
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <div className="flex p-4 justify-center">
          <Pagination
            dir="ltr"
            defaultPage={1}
            onChange={handleChangePage}
            count={Math.ceil(totalCount / rowsPerPage)}
          />
        </div>
      </Paper>
    </Box>
  );
}
