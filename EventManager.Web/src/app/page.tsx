"use client";
import { HomeTabs } from "@/constants/Routes";
import React from "react";
import { PageTabs } from "@/components/PageTabs";
import { About } from "./about/About";
import { PageWrapperAuth } from "@/components/PageWrapperAuth";

export default function Home() {
  return (
    <>
      <PageWrapperAuth>
        <PageTabs tabs={HomeTabs} />
        <About />
      </PageWrapperAuth>
    </>
  );
}
