"use client";
import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";
import useGlobalState from "../../state/GlobalState";
import { CallStatus } from "@/types/ApiTypes";
import { BusinessApi } from "@/services/ServicesIndex";
import { ApiGlobalStateManager } from "@/utils/ServiceStateHelper";
import Link from "next/link";
import { Pathname } from "@/constants/Routes";

export const ExpensesView: React.FunctionComponent = () => {
    const { businessStateList, setBusinessStateList } = useGlobalState();

    React.useEffect(() => {
        if (!(businessStateList?.data?.length > 0)) {
            ApiGlobalStateManager(BusinessApi.GetMyBusinessList(), setBusinessStateList);
        }
    }, [])

    return (
        <>
            {businessStateList.status === CallStatus.Success ? (
                businessStateList.data.length > 0 ? (
                    businessStateList.data.map((business, index) => (
                        // <BusinessDisplay key={index} {...business} />
                        <span key={index}>Hello</span>
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
