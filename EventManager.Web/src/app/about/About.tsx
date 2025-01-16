"use client";
import { PageCard } from "@/components/PageCard";
import { Pathname } from "@/constants/Routes";
import { Typography } from "@mui/material";
import React from "react";

const pagecardStyle = {
  padding: "1rem",
  paddingBottom: "0.5rem",
};

export const About: React.FunctionComponent = () => {
  return (
    <>
      <PageCard>
        <div style={pagecardStyle}>
          <Typography variant="h6" gutterBottom>
            Welcome to the Navodaya Alumni Portal.
          </Typography>
          <Typography variant="subtitle1" color="textSecondary" gutterBottom>
            Secure and Privacy focused alumni portal for Navodayans. Built with love by Navodayans for Navodayans from Sitamarhi Bihar.
          </Typography>
          <Typography variant="subtitle1" gutterBottom>
            How to use the portal:
          </Typography>
          <Typography component={"div"} variant="body2" color="textSecondary">
            <ul style={{ paddingLeft: '20px' }}>
              <li>Menu items are available on the top right corner of the page.</li>
              <li>Click on a menu item to navigate to the desired page.</li>
              <li>Use the tabs on the page to navigate to different sections.</li>
            </ul>
          </Typography>
        </div>
        <div style={pagecardStyle}>
          <Typography variant="subtitle1" gutterBottom>
            Features available on this portal:
          </Typography>
          <ul style={{ paddingLeft: '20px' }}>
            <li>
              <Typography variant="body1">
                <b>Profile:</b> [<a href={Pathname.Profile}>View</a>] | [<a href={Pathname.Profile_Edit}>Update</a>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Manage your profile and keep your information private. Your profile information will be used to create your digital identity badge.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>NearBy:</b> [<a href={Pathname.NearBy}>Find Navodayans NearBy</a>] | [<a href={Pathname.NearBy_Business}>Find Alumni Business</a>] | [<a href={Pathname.NearBy_Medico}>Find Alumni Doctors</a>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Find Navodaya alumni, businesses, and doctors near you.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Business:</b> [<a href={Pathname.Business}>Manage your business</a>] | [<a href={Pathname.Business_Add}>Add your business</a>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Register and manage your business on the portal for more visibility.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Identity:</b> [<a href={Pathname.Identity}>Digital ID</a>] | [<a href={Pathname.Identity_Verify}>Verify ID</a>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Verify your identity and get your digital identity badge.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Security:</b> [<a href={Pathname.Security}>Offline OTP</a>] | [<a href={Pathname.Security_Reset}>Reset Authentication</a>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Manage your security settings and reset your authentication.
              </Typography>
            </li>
          </ul>
        </div>
      </PageCard>
    </>
  );
};
