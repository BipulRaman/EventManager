"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NearByTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { NearByBusiness } from './NearByBusiness';

const NearbyPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NearByTabs} />
            <NearByBusiness />
        </PageWrapperAuth>
    );
};

export default NearbyPage;
