"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NewsfeedTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { NewsfeedPrivateView } from './NewsfeedPrivateView';

const NewsfeedManagePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NewsfeedTabs} />
            <NewsfeedPrivateView />
        </PageWrapperAuth>
    );
};

export default NewsfeedManagePage;