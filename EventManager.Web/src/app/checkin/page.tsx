"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { CheckInTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';

const CheckInPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={CheckInTabs} />
            {/* <BusinessView /> */}
        </PageWrapperAuth>
    );
};

export default CheckInPage;
