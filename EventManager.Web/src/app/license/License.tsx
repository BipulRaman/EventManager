import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";

export const License: React.FunctionComponent = () => {
  return (
      <PageCard>
        <Typography variant="h6">LICENSE</Typography>
        <br />
        <Typography component={"p"}>
          Copyright &copy; 2011-{new Date().getFullYear()} by Navodayan&apos;s App. All rights reserved.
        </Typography>
        <Typography component={"p"}>
          NOTICE: All information contained herein is, and remains the property of Navodayan&apos;s App. The intellectual and
          technical concepts contained herein are proprietary to Navodayan&apos;s App and are protected by trade secret or
          copyright law. Dissemination of this information or reproduction of this material is strictly forbidden unless
          prior written permission is obtained from Navodayan&apos;s App.
        </Typography>
      </PageCard>
  );
};
