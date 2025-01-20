"use client";
import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";
import { BusinessDisplay } from "./BusinessDisplay";
import useGlobalState from "../../state/GlobalState";
import { CallStatus } from "@/types/ApiTypes";
import { BusinessServices } from "@/services/ServicesIndex";
import { ApiGlobalStateManager } from "@/utils/ServiceStateHelper";
import Link from "next/link";
import { Pathname } from "@/constants/Routes";
export const BusinessView: React.FunctionComponent = () => {
    const { businessStateList, setBusinessStateList } = useGlobalState();

    React.useEffect(() => {
        if (!(businessStateList?.data?.length > 0)) {
            ApiGlobalStateManager(BusinessServices.GetMyBusinessList(), setBusinessStateList);
        }
    }, [])

    return (
        <>
            {businessStateList.status === CallStatus.Success ? (
                businessStateList.data.length > 0 ? (
                    businessStateList.data.map((business, index) => (
                        <BusinessDisplay key={index} {...business} />
                    ))
                ) : (
                    <PageCard><Typography variant="body1">You have not added your business yet. Add it now at <Link href={Pathname.Business_Add}>Add Business</Link> page.</Typography></PageCard>
                )
            ) : businessStateList.status === CallStatus.InProgress ? (
                <PageCard><Typography variant="body1">Loading...</Typography></PageCard>
            ) : businessStateList.status === CallStatus.Failure ? (
                <PageCard><Typography variant="body1">Error loading businesses. Please try again later.</Typography></PageCard>
            ) : (
                <PageCard><Typography variant="body1">You have not added your business yet. Add it now at <Link href={Pathname.Business_Add}>Add Business</Link> page.</Typography></PageCard>
            )}
        </>
    );
};
