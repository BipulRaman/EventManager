/* eslint-disable @next/next/no-head-element */
"use client";
import React from "react";
import { UpdateKeyContext, useKeyContext } from "@/utils/KeyContextHelper";
import { AuthContextProvider } from "@/utils/AuthHelper";
import { Layout } from "./Layout";
import { usePathname } from "next/navigation";
import { RoutesMetadata } from "@/constants/Routes";

export interface LayoutMainProps {
    children: React.ReactNode
}

export const LayoutMain: React.FC<LayoutMainProps> = (props) => {
    const pathname = usePathname();
    const matchedRouteData = RoutesMetadata.find(i => i.route === pathname);
    const defaultData = { title: "Navodaya Alumni App", description: "The official website of Navodaya Alumni App." };
    const { updateKey } = useKeyContext();

    return (
        <html lang="en">
            <head>
                <title>{matchedRouteData?.title || defaultData.title}</title>
                <meta name="og:title" content={matchedRouteData?.title || defaultData.title} />
                <meta name="twitter:title" content={matchedRouteData?.title || defaultData.title} />
                <meta name="description" content={matchedRouteData?.description || defaultData.description} />
                <meta name="og:description" content={matchedRouteData?.description || defaultData.description} />
                <meta name="twitter:description" content={matchedRouteData?.description || defaultData.description} />
                <meta name="og:url" content={pathname} />
                <meta name="twitter:url" content={pathname} />
                <link rel="canonical" href={pathname} />
            </head>
            <body>
                <AuthContextProvider>
                    <UpdateKeyContext.Provider value={updateKey}>
                        <Layout>{props.children}</Layout>
                    </UpdateKeyContext.Provider>
                </AuthContextProvider>
            </body>
        </html>
    );
}