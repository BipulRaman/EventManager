"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { CheckInTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { CheckinVenue } from './CheckinVenue';

const CheckInPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={CheckInTabs} />
            <CheckinVenue />
        </PageWrapperAuth>
    );
};

export default CheckInPage;
