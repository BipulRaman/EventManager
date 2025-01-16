"use client";
import React from "react";
import { List, ListItemButton, ListItemIcon, Divider, ListItemText, ListItem } from "@mui/material";
import { usePathname } from "next/navigation";
import Link from "next/link";
import { SidebarNavFooter } from "./SidebarNavFooter";
import { AdminPages, InvitedUserPages, PrivatePages, PublicPages, sidebarNavFooterProps } from "../../../LayoutConfig";
import { GetUserRoles } from "@/utils/TokenHelper";
import { Roles } from "@/types/ApiTypes";


const listItemButtonActiveClassName = "Mui-selected";

const listItemStyle: React.CSSProperties = {
  padding: 0,
};

const linkStyle: React.CSSProperties = {
  textDecoration: "none",
  width: "100%",
};

const linkTextStyle: React.CSSProperties = {
  fontWeight: 500,
  letterSpacing: "normal",
  lineHeight: "22px",
  color: "#757575",
};

const uniqueKey = (pre: number): string => {
  return `${pre}_${new Date().getTime()}`;
}

export interface ISocialLinks {
  Github: string;
  LinkedIn: string;
  Twitter: string;
  YouTube: string;
  Telegram: string;
}

export interface IPages {
  title: string;
  href: string;
  icon: React.ReactNode;
}

export const SidebarNav: React.FunctionComponent = () => {
  const pathname = usePathname() || "";
  const [pathName, setPathName] = React.useState("");

  React.useEffect(() => {
    setPathName(pathname);
  }, [pathname]);
  const userRoles = GetUserRoles();
  const isAdmin = userRoles.includes(Roles.Admin);
  const isInvitedUser = userRoles.includes(Roles.InvitedUser);
  const isUser = userRoles.includes(Roles.User);
  let currentPath = pathName;
  if (pathName.length > 1 && pathName.endsWith("/")) {
    currentPath = pathName.slice(0, -1);
  }

  return (
    <>
      <div>
        {
          isInvitedUser ? (<>
            <List component={"div"}>
              {InvitedUserPages.map(function (page: IPages, index: number) {
                return (
                  <ListItem component={"div"} style={listItemStyle} key={uniqueKey(index)}>
                    <React.Fragment >
                      <ListItemButton component="a" href={page.href} aria-label={page.title} className={currentPath === page.href ? listItemButtonActiveClassName : ""} style={linkStyle}>
                        <ListItemIcon>{page.icon}</ListItemIcon>
                        <ListItemText primary={<span style={linkTextStyle}>{page.title}</span>} />
                      </ListItemButton>
                    </React.Fragment>
                  </ListItem>
                );
              })}
            </List>
          </>) : (<>
            <List component={"div"}>
              {PublicPages.map(function (page: IPages, index: number) {
                return (
                  <ListItem component={"div"} style={listItemStyle} key={uniqueKey(index)}>
                    <React.Fragment >
                      <ListItemButton component={Link} href={page.href} aria-label={page.title} className={currentPath === page.href ? listItemButtonActiveClassName : ""} style={linkStyle}>
                        <ListItemIcon>{page.icon}</ListItemIcon>
                        <ListItemText primary={<span style={linkTextStyle}>{page.title}</span>} />
                      </ListItemButton>
                    </React.Fragment>
                  </ListItem>
                );
              })}
            </List>
          </>)
        }
        {
          isAdmin && AdminPages && AdminPages.length > 0 &&
          (
            <>
              <Divider />
              <List component={"div"}>
                {AdminPages.map(function (page: IPages, index: number) {
                  return (
                    <ListItem component={"div"} style={listItemStyle} key={uniqueKey(index)}>
                      <React.Fragment >
                        <ListItemButton component={Link} href={page.href} aria-label={page.title} className={currentPath === page.href ? listItemButtonActiveClassName : ""} style={linkStyle}>
                          <ListItemIcon>{page.icon}</ListItemIcon>
                          <ListItemText primary={<span style={linkTextStyle}>{page.title}</span>} />
                        </ListItemButton>
                      </React.Fragment>
                    </ListItem>
                  );
                })}
              </List>
            </>
          )
        }
        <Divider />
        {
          isUser &&
          (
            <>
              <List component={"div"}>
                {PrivatePages.map(function (page: IPages, index: number) {
                  return (
                    <ListItem component={"div"} style={listItemStyle} key={uniqueKey(index)}>
                      <React.Fragment >
                        <ListItemButton component={Link} href={page.href} aria-label={page.title} className={currentPath === page.href ? listItemButtonActiveClassName : ""} style={linkStyle}>
                          <ListItemIcon>{page.icon}</ListItemIcon>
                          <ListItemText primary={<span style={linkTextStyle}>{page.title}</span>} />
                        </ListItemButton>
                      </React.Fragment>
                    </ListItem>
                  );
                })}
              </List>
            </>
          )
        }
      </div>

      <SidebarNavFooter {...sidebarNavFooterProps} />
    </>
  );
};
