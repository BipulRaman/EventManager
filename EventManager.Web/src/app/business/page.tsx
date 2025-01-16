"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { BusinessTabs } from '@/constants/Routes';
import { BusinessView } from './BusinessView';
import { PageTabs } from '@/components/PageTabs';

const BusinessPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={BusinessTabs} />
            <BusinessView />
        </PageWrapperAuth>
    );
};

export default BusinessPage;
