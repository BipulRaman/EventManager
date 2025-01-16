"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { EventsIndex } from './EventsIndex';

const EventsPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <EventsIndex />
        </PageWrapperAuth>
    );
};

export default EventsPage;