"use client";
import React from "react";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Box, Tab } from "@mui/material";
import { IdentityView } from "@/app/identity/IdentityView";

const contentStyle: React.CSSProperties = {
  display: "flex",
  justifyContent: "center",
}

export const RightColumn: React.FunctionComponent = () => {
  return (
    <>
      <TabContext value={"1"}>
        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
          <TabList aria-label="identity" centered>
            <Tab label="Digital ID" value="1" />
          </TabList>
        </Box>
        <TabPanel value="1"><div style={contentStyle}><IdentityView /></div></TabPanel>
      </TabContext>
    </>
  );
};
