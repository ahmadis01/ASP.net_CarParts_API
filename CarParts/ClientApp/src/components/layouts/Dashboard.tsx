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
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { NavLink } from 'react-router-dom';
import { Avatar, Slide, TextField, useScrollTrigger } from '@mui/material';
import navLinks from '@/Navigation'

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
                {navLinks.map((item, index) => (
                    <NavLink to={{ pathname: item.path }} end key={item.path} >
                        {
                            ({ isActive }) => {
                                return (
                                    <ListItem>
                                        <ListItemButton
                                            sx={({ palette }: any) => ({
                                                py: 0.6,
                                                px: 2,
                                                borderRadius: "1rem",
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
                                                <item.icon />
                                            </ListItemIcon>
                                            <ListItemText primary={item.text} />
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
                        <Box display={'flex'} justifyContent='space-between' width={'100%'} >

                            <TextField sx={{ minWidth: 400 }} placeholder='ابحث عن قطع , سيارات , زبائن ... ' size='small' variant='standard'></TextField>
                            <Avatar title='ENG.Mohammad Khayata 😎' ></Avatar>

                        </Box>

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