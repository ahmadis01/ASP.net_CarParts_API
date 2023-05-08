import PartsTable from "@/components/parts/PartsTable";
import { PartApi } from "@/api/Part";
import { useQuery } from "react-query";
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
import { useEffect, useState } from "react";
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
  const [params, setParams] = useState<GetAllPartsParams>({
    ...new GetAllPartsParams(),
  });
  const [customers, setCustomers] = useState<CustomerItem[]>([]);
  const [partsToInvoice, setPartsToInvoice] = useState<PartItem[]>([]);
  const [invoiceDialog, setInvoiceDialog] = useState(false);
  const [total, setTotal] = useState(0);
  const [searchParams, setSearchParams] = useSearchParams();
  useEffect(() => {
    searchParams.forEach((value: string, key: string) => {
      setParams({ ...params, [key]: value });
    });
  }, []);

  const brands = useSelector<RootState, BrandItem[]>((s) => s.brand.brands);

  const { refetch } = useQuery(
    ["part", params.PageNumber],
    () =>
      PartApi.getParts({
        ...params,
      }),
    {
      onSuccess(data) {
        setParts(data.parts);
        setTotal(data.totalNumber);
      },
    }
  );

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

  const onPaginationChage = (pageNumber: number, pageSize: number) => {
    console.log(pageNumber);
    setParams((ol) => ({
      ...ol,
      PageSize: pageSize,
      PageNumber: pageNumber,
    }));
    refetch();
  };

  const showInvoiceDialog = (data: PartItem[]) => {
    console.log("invoice", data);
    setInvoiceDialog(true);
    setPartsToInvoice(data);
  };
  useEffect(() => {
    console.log("fuck react", params);
    refetch();
  }, [params]);

  return (
    <div>
      <Card className="mb-4">
        <div className="flex flex-col p-4 gap-4 ">
          <div className="flex justify-between">
            <Typography variant="h2" fontWeight={"500"} fontSize={24}>
              قطع السيارات
            </Typography>
            <div className="flex gap-4">
              <Button
                onClick={() => setParams(new GetAllPartsParams())}
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
              setParams({ ...params, [key]: value });
            }}
          />
        </div>
      </Card>
      <CreateInvoice
        onSubmit={() => {}}
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
