"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { PageTabs } from '../../../components/PageTabs';
import { BusinessTabs } from '@/constants/Routes';
import { BusinessUpdate } from './BusinessUpdate';

const BusinessEditPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={BusinessTabs} />
            <BusinessUpdate />
        </PageWrapperAuth>
    );
};

export default BusinessEditPage;
