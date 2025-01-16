"use client";
import React from "react";
import { Divider, Drawer, useMediaQuery, useTheme } from "@mui/material";
import { Profile } from "./child/Profile";
import { SidebarNav } from "./child/SidebarNav";
import { profileProps } from "../../LayoutConfig";

const drawerPaperMobileStyle: React.CSSProperties = {
  width: 280,
  marginTop: 0,
  height: "100%",
  border: "none",
  boxShadow: "0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12)",
};

const drawerPaperDesktopStyle: React.CSSProperties = {
  width: 280,
  marginTop: "55px",
  height: "calc(100% - 55px)",
  border: "none",
  boxShadow: "0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12)",
};

const rootStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  height: "100%",
  padding: 15,
};

export interface SidebarProps {
  open: boolean;
  variant: "permanent" | "persistent" | "temporary" | undefined;
  onClose(): void;
}

export const Sidebar: React.FC<SidebarProps> = (props) => {
  const { open, variant, onClose } = props;
  const theme = useTheme();
  const isDesktop = useMediaQuery(theme.breakpoints.up("lg"), {
    defaultMatches: true,
  });

  return (
    <>
      {props !== undefined && (
        <>
          <Drawer
            anchor="left"
            PaperProps={{ style: isDesktop ? drawerPaperDesktopStyle : drawerPaperMobileStyle }}
            onClose={onClose}
            open={open}
            variant={variant}
            component={"div"}
          >
            <div style={rootStyle} onClick={onClose}>
              <Profile {...profileProps} />
              <Divider />
              <SidebarNav />
            </div>
          </Drawer>
        </>
      )}
    </>
  );
};
