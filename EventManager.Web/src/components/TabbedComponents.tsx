"use client";
import * as React from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
    idPrefix: string;
}

const panelBoxStyle: React.CSSProperties = {
    marginTop: 10,
}

function CustomTabPanel(props: TabPanelProps) {
    const { children, value, index, idPrefix, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`${idPrefix}-tabpanel-${index}`}
            aria-labelledby={`${idPrefix}-tab-${index}`}
            {...other}
            style={panelBoxStyle}
        >
            {value === index && (
                <Box style={panelBoxStyle}>
                    <div>{children}</div>
                </Box>
            )}
        </div>
    );
}

function a11yProps(index: number, idPrefix: string) {
    return {
        id: `${idPrefix}-tab-${index}`,
        'aria-controls': `${idPrefix}-tabpanel-${index}`,
    };
}

export interface TabbedComponent {
    Position: number;
    Label: string;
    Component: React.ReactNode;
}

export interface TabbedComponentsProps {
    Components: TabbedComponent[];
}

export const TabbedComponents = (props: TabbedComponentsProps) => {
    const [value, setValue] = React.useState(0);
    const { Components } = props;
    const idPrefix = Components[0].Label.replace(/\s/g, "").toLowerCase();

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <Box sx={{ width: '100%' }}>
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" centered>
                    {Components.map((component, index) => (
                        <Tab key={index} label={component.Label} {...a11yProps(index, idPrefix)} />
                    ))}
                </Tabs>
            </Box>
            <div style={{ justifyContent: 'center' }}>
                {Components.map((component, index) => (
                    <CustomTabPanel key={index} value={value} index={index} idPrefix={idPrefix}>
                        {component.Component}
                    </CustomTabPanel>
                ))}
            </div>
        </Box>

    );
}
