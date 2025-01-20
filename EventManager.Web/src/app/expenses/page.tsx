"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ExpensesTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { ExpensesView } from './ExpensesView';

const ExpensePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ExpensesTabs} />
            <ExpensesView />
        </PageWrapperAuth>
    );
};

export default ExpensePage;
