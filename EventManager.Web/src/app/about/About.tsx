"use client";
import React from "react";
import { PageCard } from "@/components/PageCard";
import { Pathname } from "@/constants/Routes";
import { Typography } from "@mui/material";
import Link from "next/link";

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
            Welcome to the Samagam Portal.
          </Typography>
          <Typography variant="subtitle1" color="textSecondary" gutterBottom>
            Secure and Privacy focused event management portal. Built with love by Navodayans for Navodayans from Bihar.
          </Typography>
          <Typography variant="subtitle1" gutterBottom>
            How to use the portal:
          </Typography>
          <Typography component={"div"} variant="body2" color="textSecondary">
            <ul style={{ paddingLeft: '20px' }}>
              <li>Menu items are available at the bottom of the page in mobile view and on left side in Desktop view.</li>
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
                <b>Profile:</b> [<Link href={Pathname.Profile}>View</Link>] | [<Link href={Pathname.Profile_Edit}>Update</Link>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Manage your profile and keep your information private. Your profile information will be used to create your digital identity badge.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Expenses:</b> [<Link href={Pathname.Expense}>View Expenses</Link>] | [<Link href={Pathname.Expense_Add}>Add Expense</Link>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Manage and View Expenses.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Check-In:</b> [<Link href={Pathname.CheckIn}>Attendee CheckIn</Link>]
              </Typography>
              <Typography variant="body2" color="textSecondary">
                Checkin to the event.
              </Typography>
            </li>
            <li>
              <Typography variant="body1">
                <b>Security:</b> [<Link href={Pathname.Security}>Offline OTP</Link>] | [<Link href={Pathname.Security_Reset}>Reset Authentication</Link>]
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
