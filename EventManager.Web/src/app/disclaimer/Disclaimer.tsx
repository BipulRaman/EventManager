"use client";
import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";

export const Disclaimer: React.FunctionComponent = () => {
  return (
    <>
      <PageCard>
        <Typography variant="h6">Disclaimer</Typography>
        <br />
        <Typography variant="body1">
          The views, thoughts, and opinions expressed in the blog / article or on pages of this website belong solely to
          the author, and not necessarily to the author&apos;s employer, organization, committee or other group or
          individual.
        </Typography>
      </PageCard>
      <PageCard>
        <Typography variant="h6">Objection Settlement</Typography>
        <br />
        <Typography variant="body1">
          In case of any objection over any content of this website, Please reach out to us{" "}
          <a href="mailto:info@nvsalumni.in">via Email</a>.
        </Typography>
      </PageCard>
    </>
  );
};
