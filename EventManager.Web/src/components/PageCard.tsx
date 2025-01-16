"use client";
import React from "react";
import { Paper } from "@mui/material";

const paperStyle: React.CSSProperties = {
  padding: 15,
  marginBottom: 15,
};

export interface PageCardProps {
  children: React.ReactNode;
  style?: React.CSSProperties;
}

export const PageCard: React.FunctionComponent<PageCardProps> = (props: PageCardProps) => {
  return (
    <>

      <Paper component="div" elevation={3} style={paperStyle}>
        <div style={props.style}>
          {props.children}
        </div>
      </Paper>

    </>
  );
};
