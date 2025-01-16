"use client";
import React from "react";
import { Typography } from "@mui/material";
import FavoriteIcon from "@mui/icons-material/Favorite";
import Link from "next/link";
import Image from "next/image";

const rootStyle: React.CSSProperties = {
  padding: "10px 5px 70px 5px",
};

const desktopRootStyle: React.CSSProperties = {
  padding: "10px 5px 10px 5px",
};

const iconStyle: React.CSSProperties = {
  fontSize: 14,
  color: "#ea4335",
};

export interface FooterProps {
  aboutUrl: string;
  copyrightOwner: string;
  licenseUrl: string;
  privacyPolicyUrl: string;
  disclaimerUrl: string;
  indiaFlagSvgUrl: string;
}

export const Footer = (props: FooterProps) => {
  const [isMobile, setIsMobile] = React.useState(false);

  React.useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 768);
    };

    handleResize(); // Set initial value
    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  const appliedRootStyle = isMobile ? rootStyle : desktopRootStyle;
  
  return (
    <div style={appliedRootStyle}>
      <Typography component={"span"} variant="body2" color="textSecondary">
        <Link href={props.licenseUrl}>© {props.copyrightOwner} {new Date().getFullYear()}</Link>
        {" | "}
        <Link href={props.aboutUrl}>About</Link>
        {" | "}
        <Link href={props.privacyPolicyUrl}>Privacy Policy</Link>
        {" | "}
        <Link href={props.disclaimerUrl}>Disclaimer</Link>
        {" | Made with "}
        <FavoriteIcon style={iconStyle} />
        {" in Bharat  "}
        <Image src={props.indiaFlagSvgUrl} alt="India" height="11" width="20" />
        {"."}
      </Typography>
    </div>
  );
};
