"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { PageTabs } from '../../../components/PageTabs';
import { BusinessTabs } from '@/constants/Routes';
import { BusinessCreate } from './BusinessCreate';

const BusinessAddPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={BusinessTabs} />
            <BusinessCreate />
        </PageWrapperAuth>
    );
};

export default BusinessAddPage;
