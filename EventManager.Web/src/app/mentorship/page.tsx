"use client";
import { NextPage } from 'next';
import { MentorshipIndex } from './MentorshipIndex';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';

const MentorshipPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <MentorshipIndex />
        </PageWrapperAuth>
    );
};

export default MentorshipPage;