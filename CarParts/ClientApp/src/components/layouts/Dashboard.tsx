import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import Drawer from "@mui/material/Drawer";
import MenuIcon from "@mui/icons-material/Menu";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import { NavLink } from "react-router-dom";
import { IconButton, Slide, useScrollTrigger } from "@mui/material";
import navLinks from "@/Navigation";

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
      <Toolbar sx={{
        display:'flex',
        justifyContent:'center',
        gap:2
      }}>
        <Typography fontWeight={'bold'} fontSize={24}>Auto Parts</Typography>
        <img alt='logo' width={'56'} src='/Asset 1.svg'></img>
      </Toolbar>
      <List>
        {navLinks.map((item, index) => (
          <NavLink
            to={{ pathname: item.path, search: item.params ?? "" }}
            end
            key={item.path}
          >
            {({ isActive }) => {
              return (
                <ListItem>
                  <ListItemButton
                    sx={({ palette }: any) => ({
                      py: 0.8,
                      px: 2,
                      borderRadius: "0.5rem",
                      color: isActive ? "white" : palette.grey["700"],
                      "&.Mui-selected , &.Mui-selected:hover": {
                        backgroundColor: palette.primary.main,
                      },
                    })}
                    selected={isActive}
                  >
                    <ListItemIcon
                      sx={({ palette }) => ({
                        color: isActive ? "white" : palette.grey["700"],
                      })}
                    >
                      <item.icon />
                    </ListItemIcon>
                    <ListItemText primary={item.text} />
                  </ListItemButton>
                </ListItem>
              );
            }}
          </NavLink>
        ))}
      </List>
    </Box>
  );

  const container =
    window !== undefined ? () => window().document.body : undefined;

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <HideOnScroll {...props}>
        <AppBar
          className="border-b"
          sx={{
            background: (teme) => teme.palette.background.paper,
            width: { md: `calc(100% - ${drawerWidth}px)` },
            ml: { md: `${drawerWidth}px` },
            borderLeft:0
          }}
        >
          <Toolbar>
            <Box display={"flex"} justifyContent="space-between" width={"100%"}>
              <IconButton
                sx={{
                  display: {
                    xs: "block",
                    md: "none",
                  },
                }}
                onClick={() => setMobileOpen(true)}
              >
                <MenuIcon></MenuIcon>
              </IconButton>
            </Box>
          </Toolbar>
        </AppBar>
      </HideOnScroll>
      <Box
        component="nav"
        sx={{ width: { md: drawerWidth }, flexShrink: { md: 0 } }}
        aria-label="mailbox folders"
      >
        <Drawer
          dir="rtl"
          container={container}
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          anchor="right"
          PaperProps={{ style: { left: "unset", right: 0 } }}
          ModalProps={{
            keepMounted: true, // Better open performance on mobile.
          }}
          sx={({ palette }) => ({
            display: { xs: "block", md: "none" },
            "& .MuiDrawer-paper": {
              boxSizing: "border-box",
              width: drawerWidth,
            },
          })}
        >
          {drawer}
        </Drawer>

        <Drawer
          variant="permanent"
          sx={({ palette }) => ({
            display: { xs: "none", md: "block" },
            "& .MuiDrawer-paper": {
              boxSizing: "border-box",
              width: drawerWidth,
            },
          })}
          open
        >
          {drawer}
        </Drawer>
      </Box>

      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 1,
          width: {
            md: `calc(100% - ${drawerWidth}px)`,
            xs: `100%`,
          },
        }}
      >
        <Toolbar />
        {props.children}
      </Box>
    </Box>
  );
}
