"use client";
import React from "react";
import { AppBar, Toolbar, IconButton, Typography, Stack, useMediaQuery, useTheme } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Link from "next/link";
import { LoginLogoutButton } from "./LoginLogoutButton";
import NotificationsIcon from '@mui/icons-material/Notifications';
import { Pathname } from "../../constants/Routes";
import Image from "next/image";

const rootStyle: React.CSSProperties = {
  boxShadow: "0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12)",
  height: 55,
};

const toolbarStyle: React.CSSProperties = {
  height: 55,
  minHeight: 55,
};

const flexGrowStyle: React.CSSProperties = {
  flexGrow: 1,
};

const siteTitleStyle: React.CSSProperties = {
  color: "#FFFFFF",
};

const notificationStyle: React.CSSProperties = {
  color: "#FFFFFF",
}

const logoStyle: React.CSSProperties = {
  height: 40,
  width: 32,
  paddingTop: 5,
  marginRight: 4,
};

const linkStyle: React.CSSProperties = {
  textDecoration: "none",
};

export interface TopbarProps {
  onSidebarOpen(): void;
  logoUrl: string;
  siteTitle: string;
}

export const Topbar: React.FC<TopbarProps> = (props) => {
  const showNotificationButton = false;
  const showLoginLogoutButton = true;
  const handleNotificationClick = () => {
  }

  const theme = useTheme();
  const isDesktop = useMediaQuery(theme.breakpoints.up("lg"), {
    defaultMatches: true,
  });

  return (
    <>
      <AppBar style={rootStyle}>
        <Toolbar style={toolbarStyle}>
          <Stack direction="row" alignItems="center" spacing={1}>
            {
              props.logoUrl === "" ? null : (
                <Link href={Pathname.Root} style={linkStyle}>
                  <Image src={props.logoUrl} alt={props.siteTitle} style={logoStyle} />
                </Link>
              )
            }
            <Link href={Pathname.Root} style={linkStyle}>
              <Typography component={"span"} variant="h5" style={siteTitleStyle}>
                {props.siteTitle}
              </Typography>
            </Link>
          </Stack>

          <div style={flexGrowStyle} />
          {
            showNotificationButton && (
              <IconButton aria-label="notification" style={notificationStyle} onClick={handleNotificationClick}>
                <NotificationsIcon />
              </IconButton>
            )
          }
          {
            (showLoginLogoutButton && isDesktop) && (
              <LoginLogoutButton />
            )
          }
          {isDesktop ? null : (
            <IconButton color="inherit" onClick={props.onSidebarOpen} aria-label="Menu">
              <MenuIcon />
            </IconButton>
          )}
        </Toolbar>
      </AppBar>
    </>
  );
};
