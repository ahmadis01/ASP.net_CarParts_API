import PartsTable from "@/components/parts/PartsTable";
import { PartApi } from "@/api/Part";
import { useQuery, useQueryClient } from "react-query";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Button,
  Card,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import AddCarPart from "@/components/parts/AddCarPart";
import { useSelector } from "react-redux";
import { RootState } from "@/store";
import { CarApi } from "@/api/Car";
import { useEffect, useMemo, useState } from "react";
import { GetAllCar } from "@/api/Car/dto";
import { BrandItem } from "@/api/Brand/dto";
import { CategoryApi } from "@/api/Category";
import { InventoryApi } from "@/api/Inventory";
import { CategoryItem } from "@/api/Category/dto";
import { InventoryItem } from "@/api/Inventory/dto";
import { GetAllPartsParams, PartItem } from "@/api/Part/GetAllDto";
import { BrandApi } from "@/api/Brand";
import PartsFilter from "@/components/parts/PartsFilter";
import { Refresh } from "@mui/icons-material";
import CreateInvoice from "@/components/invoice/CreateInvoice";
import { CustomerItem } from "@/api/Customer/GetAll";
import { CustomerApi } from "@/api/Customer";
import { useParams, useSearchParams } from "react-router-dom";

function Products() {
  const [carsList, setCarsList] = useState<GetAllCar[]>([]);
  const [categoriesList, setcategoriesList] = useState<CategoryItem[]>([]);
  const [inventoriesList, setinventoriesList] = useState<InventoryItem[]>([]);
  const [brandsList, setBrandsList] = useState<BrandItem[]>([]);
  const [parts, setParts] = useState<PartItem[]>([]);

  const [customers, setCustomers] = useState<CustomerItem[]>([]);
  const [partsToInvoice, setPartsToInvoice] = useState<PartItem[]>([]);
  const [invoiceDialog, setInvoiceDialog] = useState(false);
  const [total, setTotal] = useState(0);
  const [searchParams, setSearchParams] = useSearchParams();
  const params = useMemo<GetAllPartsParams>(
    () => Object.fromEntries(searchParams) as any,
    [searchParams]
  );

  searchParams.getAll;

  const brands = useSelector<RootState, BrandItem[]>((s) => s.brand.brands);

  const { refetch } = useQuery(
    ["part", params.PageNumber],
    () => PartApi.getParts(params),
    {
      onSuccess(data) {
        setParts(data.parts);
        setTotal(data.totalNumber);
      },
    }
  );

  const {invalidateQueries} = useQueryClient()

  const carQuery = useQuery("car", CarApi.fetchCars, {
    onSuccess: (cars) => setCarsList(cars),
  });

  const categoryQuery = useQuery<CategoryItem[]>(
    "category",
    CategoryApi.GetAll,
    {
      onSuccess: (data) => setcategoriesList(data),
    }
  );

  const inventoryQuery = useQuery<InventoryItem[]>(
    "inventory",
    InventoryApi.GetAll,
    {
      onSuccess: (data) => setinventoriesList(data),
    }
  );

  const brandQuery = useQuery<BrandItem[]>("brands", BrandApi.fetchBrands, {
    onSuccess: (data) => {
      setBrandsList(data);
    },
  });
  const customerQuery = useQuery({
    queryFn: CustomerApi.fetchCustomers,
    queryKey: "customer",
    onSuccess: (data) => {
      setCustomers(data);
    },
  });

  const onPaginationChage = (PageNumber: number, PageSize: number) => {
    searchParams.set("PageSize", PageSize.toString());
    searchParams.set("PageNumber", PageNumber.toString());
    setSearchParams(searchParams);
    refetch();
  };

  const showInvoiceDialog = (data: PartItem[]) => {
    console.log("invoice", data);
    setInvoiceDialog(true);
    setPartsToInvoice(data);
  };
  useEffect(() => {
    refetch();
  }, [JSON.stringify(params)]);

  return (
    <div>
      <Card className="mb-4" >
        <div className="flex flex-col p-4 gap-4 ">
          <div className="flex justify-between">
            <Typography variant="h2" fontWeight={"bold"} fontSize={24}>
              قطع السيارات
            </Typography>
            <div className="flex gap-4">
              <Button
                onClick={() => {
                 
                  setSearchParams({
                    PageSize:'5',
                    PageNumber:'1'
                  })
                }}
                endIcon={<Refresh />}
              >
                تهيئة
              </Button>
              <AddCarPart
                brands={brands}
                inventories={inventoriesList}
                categories={categoriesList}
                cars={carsList}
              ></AddCarPart>
            </div>
          </div>
          {/* {JSON.stringify(params)} */}
          <PartsFilter
            {...{
              brandsList,
              carsList,
              categoriesList,
              countriesList: [],
              inventoriesList,
              params,
            }}
            onFilterChange={(key: string, value: string | null) => {
              if (value) {
                searchParams.set(key, value);
                setSearchParams(searchParams);
              } else {
                searchParams.delete(key);
              }
            }}
          />
        </div>
      </Card>
      <CreateInvoice
        onSubmit={() => {
          invalidateQueries('part')
        }}
        parts={partsToInvoice}
        customers={customers}
        onClose={(e) => setInvoiceDialog(e)}
        is={invoiceDialog}
      ></CreateInvoice>
      <PartsTable
        onGenerateInvoice={(data: PartItem[]) => showInvoiceDialog(data)}
        page={params.PageNumber}
        onPageChange={onPaginationChage}
        rows={parts}
        rowsPerPage={params.PageSize}
        totalCount={total}
      />
    </div>
  );
}

export default Products;
