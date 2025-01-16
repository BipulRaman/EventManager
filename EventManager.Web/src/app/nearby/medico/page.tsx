"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NearByTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { NearByMedico } from './NearByMedico';

const NearbyPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NearByTabs} />
            <NearByMedico />
        </PageWrapperAuth>
    );
};

export default NearbyPage;
