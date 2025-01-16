"use client";
import "./globals.css";
import { LayoutMain } from "../layout/LayoutMain";

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return <LayoutMain>{children}</LayoutMain>;
}
