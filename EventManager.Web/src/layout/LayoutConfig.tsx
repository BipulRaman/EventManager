"use client";
import React from "react";
import HomeIcon from "@mui/icons-material/Home";
import BusinessIcon from '@mui/icons-material/Business';
import PersonIcon from '@mui/icons-material/Person';
import LockPersonIcon from '@mui/icons-material/LockPerson';
import SecurityIcon from '@mui/icons-material/Security';
import BadgeIcon from '@mui/icons-material/Badge';
import { ImagePath } from "../constants/StaticFiles";
import { IPages } from "./child/Sidebar/child/SidebarNav";
import { ProfileLinks } from "../constants/ExternalLinks";
import { ProfileProps } from "./child/Sidebar/child/Profile";
import { FooterProps } from "./child/Footer";
import { TopbarProps } from "./child/Topbar";
import { SidebarNavFooterProps } from "./child/Sidebar/child/SidebarNavFooter";
import { Pathname } from "../constants/Routes";
import SearchIcon from '@mui/icons-material/Search';

export const PublicPages: IPages[] = [
  {
    title: "Home",
    href: Pathname.Root,
    icon: <HomeIcon />,
  },
  {
    title: "Profile",
    href: Pathname.Profile,
    icon: <PersonIcon />,
  },
  {
    title: "NearBy",
    href: Pathname.NearBy,
    icon: <SearchIcon />,
  },
  {
    title: "Business",
    href: Pathname.Business,
    icon: <BusinessIcon />,
  },
];

export const BottomNavPages: IPages[] = [
  {
    title: "Profile",
    href: Pathname.Profile,
    icon: <PersonIcon />,
  },
  {
    title: "NearBy",
    href: Pathname.NearBy,
    icon: <SearchIcon />,
  },
  {
    title: "Business",
    href: Pathname.Business,
    icon: <BusinessIcon />,
  },
  {
    title: "Identity",
    href: Pathname.Identity,
    icon: <BadgeIcon />,
  },
];

export const InvitedUserPages: IPages[] = [
  {
    title: "Profile",
    href: Pathname.Profile,
    icon: <PersonIcon />,
  },
  {
    title: "Logout",
    href: Pathname.Logout,
    icon: <LockPersonIcon />,
  },
];

export const AdminPages: IPages[] = [
];

export const PrivatePages: IPages[] = [
  {
    title: "Identity",
    href: Pathname.Identity,
    icon: <BadgeIcon />,
  },
  {
    title: "Security",
    href: Pathname.Security,
    icon: <SecurityIcon />,
  },
  {
    title: "Logout",
    href: Pathname.Logout,
    icon: <LockPersonIcon />,
  },
];

export const profileProps: ProfileProps = {
  name: "Navodayan's App",
  avatar: ImagePath.logo,
  bio: "Connecting Navodayans!",
};

export const footerProps: FooterProps = {
  copyrightOwner: "Alumni App",
  aboutUrl: Pathname.About,
  licenseUrl: Pathname.License,
  privacyPolicyUrl: Pathname.Privacy,
  disclaimerUrl: Pathname.Disclaimer,
  indiaFlagSvgUrl: ImagePath.indiaFlag,
}

export const sidebarNavFooterProps: SidebarNavFooterProps = {
  github: ProfileLinks.github,
  linkedIn: ProfileLinks.linkedIn,
  twitter: ProfileLinks.twitter,
  youTube: ProfileLinks.youTube,
  telegram: ProfileLinks.telegram,
  facebook: ProfileLinks.facebook,
  instagram: ProfileLinks.instagram,
}

export const topbarProps: TopbarProps = {
  logoUrl: "",
  siteTitle: "Navodayan's App",
  onSidebarOpen: function () {
    throw new Error("Function not implemented.");
  }
}