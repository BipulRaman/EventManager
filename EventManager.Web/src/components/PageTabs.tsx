"use client";
import * as React from 'react';
import Box from '@mui/material/Box';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import { useRouter, usePathname } from 'next/navigation';

interface TabConfig {
    label: string;
    path: string;
}

interface PageTabsProps {
    tabs: TabConfig[];
}

export const PageTabs: React.FC<PageTabsProps> = ({ tabs }) => {
    const [value, setValue] = React.useState(0);
    const router = useRouter();
    const pathname = usePathname();

    React.useEffect(() => {
        const tabIndex = tabs.findIndex(tab => tab.path === pathname);
        setValue(tabIndex);
    }, [pathname]);

    const handleChange = React.useCallback((_event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
        router.push(tabs[newValue].path.split('?')[0].split('#')[0]);
    }, []);

    const renderedTabs = React.useMemo(() => (
        tabs.map((tab, index) => (
            <Tab key={index} label={tab.label} />
        ))
    ), [tabs]);

    return (
        <Box sx={{ width: '100%', borderBottom: 1, borderColor: 'divider', marginBottom: 1 }}>
            <Tabs value={value} onChange={handleChange} centered>
                {renderedTabs}
            </Tabs>
        </Box>
    );
};
