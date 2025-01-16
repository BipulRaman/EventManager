"use client";
import React from "react";
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import { useRouter, usePathname } from "next/navigation";
import { IPages } from "./Sidebar/child/SidebarNav";
import MenuIcon from '@mui/icons-material/Menu';

export const boottomNavStyles: React.CSSProperties = {
  position: 'fixed',
  zIndex: 1000,
  bottom: 0,
  left: 0,
  right: 0,
  width: '100%',
  border: '1px solid #e0e0e0',
  boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.1)',
  borderRadius: '8px',
  backgroundColor: '#ffffff'
};

export interface MobileFooterNavProps {
  pages: IPages[];
  onSidebarOpen(): void;
}

export const MobileFooterNav = (props: MobileFooterNavProps) => {
  const [value, setValue] = React.useState('recents');
  const router = useRouter();
  const currentPathName = usePathname();

  // Set value on route change
  React.useEffect(() => {
    setValue(currentPathName);
  }, [currentPathName]);

  return (
    <BottomNavigation
      showLabels
      value={value}
      style={boottomNavStyles}
    >
      {props.pages.map((page: IPages) => (
        <BottomNavigationAction
          key={page.href}
          label={page.title}
          value={page.href}
          icon={page.icon}
          onClick={() => {
            setValue(page.href);
            router.push(page.href);
          }}
        />
      ))}
      <BottomNavigationAction
          label={"Menu"}
          icon={<MenuIcon />}
          onClick={() => {
            props.onSidebarOpen();
          }}
        />
    </BottomNavigation>
  );
};
