import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Drawer from '@mui/material/Drawer';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import MailIcon from '@mui/icons-material/Mail';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { NavLink } from 'react-router-dom';
import { Slide, useScrollTrigger } from '@mui/material';
import { Home, ViewCompact, Inventory, ImportExport, Store, AttachMoney, Group, Settings, Google, Web, } from '@mui/icons-material'
import DirectionsCarFilledIcon from '@mui/icons-material/DirectionsCarFilled';

const navlinks = [
    {
        text: "الرئيسية",
        path: "/",
        icon: <Home />
    },
    {
        text: "السيارات",
        path: "/cars",
        icon: <DirectionsCarFilledIcon />
    },
    {
        text: "المنتجات",
        path: "/products",
        icon: <ViewCompact />
    },
    {
        text: "الفواتير",
        path: "/invoces",
        icon: <ImportExport />
    },
    {
        text: "المحاسبة",
        path: "/accounting",
        icon: <AttachMoney />
    },
    {
        text: "العلامات التجارية",
        path: "/brands",
        icon: <Google />
    },
    {
        text: "الدول المصنعة",
        path: "/countries",
        icon: <Web />
    },
    {
        text: "الاعدادات",
        path: "/settings",
        icon: <Settings />
    },
    {
        text: "المستودعات",
        path: "/inventories",
        icon: <Inventory />
    },
    {
        text: "زبائن",
        path: "/clients",
        icon: <Inventory />
    },


]


const drawerWidth = 280;

interface Props {
    /**
     * Injected by the documentation to work in an iframe.
     * You won't need it on your project.
     */
    window?: () => Window;
}


function HideOnScroll(props: React.PropsWithChildren & any) {
    const { children, window } = props;
    // Note that you normally won't need to set the window ref as useScrollTrigger
    // will default to window.
    // This is only being set here because the demo is in an iframe.
    const trigger = useScrollTrigger({
        target: window ? window() : undefined,
    });

    return (
        <Slide appear={false} direction="down" in={!trigger}>
            {children}
        </Slide>
    );
}

export default function ResponsiveDrawer(props: React.PropsWithChildren & any) {
    const { window } = props;
    const [mobileOpen, setMobileOpen] = React.useState(false);

    const handleDrawerToggle = () => {
        setMobileOpen(!mobileOpen);
    };

    const drawer = (
        <Box>
            <Toolbar >
                <Typography color={'white'} fontSize={24}>Auto Parts </Typography>
            </Toolbar>
            <List>
                {navlinks.map(({ text, path, icon }, index) => (
                    <NavLink to={{ pathname: path }} end key={path} >
                        {
                            ({ isActive }) => {
                                return (
                                    <ListItem >
                                        <ListItemButton
                                            sx={({ palette }: any) => ({
                                                borderRadius: "15px",
                                                color: isActive ? "white" : palette.grey['700'],
                                                "&.Mui-selected , &.Mui-selected:hover": {
                                                    backgroundColor: palette.primary.main
                                                },
                                                "&:hover": {
                                                    backgroundColor: palette.secondary['600']
                                                },
                                            })}
                                            selected={isActive} >
                                            <ListItemIcon sx={({ palette }) => ({ color: isActive ? "white" : palette.grey['700'] })}>
                                                {icon}
                                            </ListItemIcon>
                                            <ListItemText primary={text} />
                                        </ListItemButton>
                                    </ListItem>
                                )
                            }
                        }

                    </NavLink>
                ))}
            </List>

        </Box >

    );

    const container = window !== undefined ? () => window().document.body : undefined;

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <HideOnScroll {...props}>
                <AppBar
                    elevation={0}
                    className='border-b'
                    sx={{
                        background: "white",
                        width: { sm: `calc(100% - ${drawerWidth}px)` },
                        ml: { sm: `${drawerWidth}px` },
                    }}>
                    <Toolbar >
                        <Typography color='#777777' variant="h6" component="div">
                            Scroll to hide App bar
                        </Typography>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Box
                component="nav"
                sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
                aria-label="mailbox folders"
            >
                <Drawer


                    container={container}
                    variant="temporary"
                    open={mobileOpen}
                    onClose={handleDrawerToggle}
                    ModalProps={{
                        keepMounted: true, // Better open performance on mobile.
                    }}
                    sx={({ palette }) => ({

                        display: { xs: 'block', sm: 'none' },
                        '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                    })}
                >
                    {drawer}
                </Drawer>
                <Drawer
                    variant="permanent"
                    sx={({ palette }) => ({
                        display: { xs: 'none', sm: 'block' },
                        '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth, backgroundColor: palette.secondary.main },
                    })}
                    open
                >
                    {drawer}
                </Drawer>
            </Box>


            <Box
                component="main"
                sx={{ flexGrow: 1, p: 0, width: { sm: `calc(100% - ${drawerWidth}px)` } }}
            >

                <Toolbar />
                {props.children}


            </Box>
        </Box>
    );
}