"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { About } from './About';

const AboutPage: NextPage = () => {
    return (
        <PageWrapper>
            <About />
        </PageWrapper>
    );
};

export default AboutPage;