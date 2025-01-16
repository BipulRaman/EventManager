"use client";
import { NextPage } from 'next';
import { PollIndex } from './PollIndex';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';

const PollPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PollIndex />
        </PageWrapperAuth>
    );
};

export default PollPage;