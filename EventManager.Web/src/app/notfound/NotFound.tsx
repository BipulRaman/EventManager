"use client";
import React from "react";
import { Typography } from "@mui/material";
import { ImagePath } from "../../constants/StaticFiles";
import { PageCard } from "../../components/PageCard";
import Image from "next/image";

const imageStyle: React.CSSProperties = {
  maxWidth: "560px",
  width: "100%",
};

export const NotFound: React.FunctionComponent = () => {
  return (
    <>
      <PageCard>
        <Typography variant="h6">That&apos;s an error ☹! We could&apos;t find the requested page on this website.</Typography>
        <br />
        <br />
        <Image src={ImagePath.error} style={imageStyle} alt="Error" />
      </PageCard>
    </>
  );
};
