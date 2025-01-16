"use client";
import React, { useEffect } from "react";
import { usePathname } from "next/navigation";
import { Login } from "../app/login/Login";
import { useAuth } from "../utils/AuthHelper";

export interface PageWrapperAuthProps {
  children: React.ReactNode;
}

export const PageWrapperAuth: React.FunctionComponent<PageWrapperAuthProps> = (props: PageWrapperAuthProps) => {
  const isAuthenticated = useAuth();
  const [isLoaded, setIsLoaded] = React.useState(false);
  const pathname = usePathname();

  useEffect(() => {
    window.scrollTo(0, 0);
    setIsLoaded(true);
  }, [pathname]);

  return (
    <>
      {
        isLoaded && (isAuthenticated ? props.children : <Login pathname={pathname} />)
      }
    </>
  );
};
