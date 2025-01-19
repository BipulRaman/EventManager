"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ExpensesTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';

const ExpensePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ExpensesTabs} />
            {/* <BusinessView /> */}
        </PageWrapperAuth>
    );
};

export default ExpensePage;
