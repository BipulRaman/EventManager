"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { NewsfeedTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { PostCreate } from './PostCreate';

const NewsfeedCreatePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={NewsfeedTabs} />
            <PostCreate />
        </PageWrapperAuth>
    );
};

export default NewsfeedCreatePage;