import { Home, ViewCompact, Inventory, ImportExport, AttachMoney, Group, Settings, Google, Web, } from '@mui/icons-material'
import DirectionsCarFilledIcon from '@mui/icons-material/DirectionsCarFilled';

export default [
    {
        text: "الرئيسية",
        path: "/",
        icon: Home 
    },
    {
        text: "السيارات",
        path: "/cars",
        icon: DirectionsCarFilledIcon 
    },
    {
        text: "القطع",
        path: "/products",
        icon: ViewCompact,
        params:"?PageSize=5&PageNumber=1",
    },
    {
        text: "الفواتير",
        path: "/invoces",
        icon: ImportExport 
    },
    {
        text: "المحاسبة",
        path: "/accounting",
        icon: AttachMoney 
    },
    {
        text: "العلامات التجارية",
        path: "/brands",
        icon: Google 
    },
    {
        text: "الدول المصنعة",
        path: "/countries",
        icon: Web 
    },
    {
        text: "الاعدادات",
        path: "/settings",
        icon: Settings 
    },
    {
        text: "المستودعات",
        path: "/inventories",
        icon: Inventory 
    },
    {
        text: "زبائن",
        path: "/clients",
        icon: Inventory 
    },


]
