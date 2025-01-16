"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NearByTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { NearByAlumni } from './NearByAlumni';

const NearbyPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NearByTabs} />
            <NearByAlumni />
        </PageWrapperAuth>
    );
};

export default NearbyPage;
