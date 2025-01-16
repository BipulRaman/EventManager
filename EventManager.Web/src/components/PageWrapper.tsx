"use client";
import React from "react";
import { usePathname } from "next/navigation";

export interface PageWrapperProps {
  children: React.ReactNode;
}

export const PageWrapper: React.FunctionComponent<PageWrapperProps> = (props: PageWrapperProps) => {
  const pathname = usePathname();

  React.useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);

  return (
    <>
      <div style={{ paddingTop: "15px", width: "100%" }}>
        {props.children}
      </div>
    </>
  );
};
