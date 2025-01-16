"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NewsfeedTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { NewsfeedPublicView } from './NewsfeedPublicView';

const NewsfeedPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NewsfeedTabs} />
            <NewsfeedPublicView />
        </PageWrapperAuth>
    );
};

export default NewsfeedPage;