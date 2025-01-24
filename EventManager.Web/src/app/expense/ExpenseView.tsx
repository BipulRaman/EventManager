"use client";
import React from "react";
import useGlobalState from "../../state/GlobalState";
import { CallStatus } from "@/types/ApiTypes";
import { ExpenseServices } from "@/services/ServicesIndex";
import { ApiGlobalStateManager } from "@/utils/ServiceStateHelper";
import { StatusMessage } from "@/components/StatusMessage";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material";
import { ISODateTimeToReadable } from "@/utils/CommonHelper";

const subdetailsStyle: React.CSSProperties = {
    fontSize: '0.7rem',
    color: 'gray',
};

export const ExpenseView: React.FunctionComponent = () => {
    const { expenseStateList, setExpenseStateList } = useGlobalState();
    const [createdById, setCreatedById] = React.useState('');
    const [expType, setExpType] = React.useState('');
    const [uniqueCreatedByNames, setUniqueCreatedByNames] = React.useState<string[]>([]);
    const [createdByNames, setCreatedByNames] = React.useState<{ [key: string]: string }>({});

    React.useEffect(() => {
        if (expenseStateList?.status !== CallStatus.Success && !(expenseStateList?.data?.length > 0)) {
            ApiGlobalStateManager(ExpenseServices.GetExpenses(), setExpenseStateList);
        }
    }, []);

    React.useEffect(() => {
        if (expenseStateList.status === CallStatus.Success && expenseStateList.data) {
            setUniqueCreatedByNames([...new Set(expenseStateList.data.map(row => row.createdBy))]);
            setCreatedByNames(expenseStateList.data.reduce((acc, row) => {
                acc[row.createdBy] = row.createdByName;
                return acc;
            }, {} as { [key: string]: string }));
        }
    }, [expenseStateList.data]);

    const handleNameChange = (event: SelectChangeEvent) => {
        setCreatedById(event.target.value as string);
    };

    const handleTypeChange = (event: SelectChangeEvent) => {
        setExpType(event.target.value as string);
    };

    const filteredData = React.useMemo(() => {
        if (expenseStateList.status !== CallStatus.Success || !expenseStateList.data) return [];
        return expenseStateList.data
            .filter(row => !createdById || row.createdBy === createdById)
            .filter(row => !expType || (expType === "Credit" ? row.amount > 0 : row.amount < 0));
    }, [expenseStateList.data, createdById, expType]);

    const totalAmount = React.useMemo(() => {
        return filteredData.reduce((total, row) => total + row.amount, 0);
    }, [filteredData]);

    return (
        <>
            <StatusMessage
                display={false}
                notStartedMessage="Loading.."
                successMessage=""
                failureMessage="Something went wrong."
                currentStatus={expenseStateList.status}
            />
            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                <InputLabel id="name-label">Filter Name</InputLabel>
                <Select
                    labelId="name-label"
                    id="name"
                    value={createdById}
                    onChange={handleNameChange}
                    label="Filter Name"
                >
                    <MenuItem value=""><em>None</em></MenuItem>
                    {uniqueCreatedByNames.map((createdBy, index) => (
                        <MenuItem key={index} value={createdBy}>{createdByNames[createdBy]}</MenuItem>
                    ))}
                </Select>
            </FormControl>
            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                <InputLabel id="type-label">Filter Type</InputLabel>
                <Select
                    labelId="type-label"
                    id="type"
                    value={expType}
                    onChange={handleTypeChange}
                    label="Filter Type"
                >
                    <MenuItem value=""><em>None</em></MenuItem>
                    <MenuItem value="Credit">Credit</MenuItem>
                    <MenuItem value="Debit">Debit</MenuItem>
                </Select>
            </FormControl>

            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 300 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>
                                <div><b>Particular</b></div>
                                <div style={subdetailsStyle}>{filteredData.length} transactions</div>
                            </TableCell>
                            <TableCell align="right">
                                <div><b>Amount (₹)</b></div>
                                <div>{totalAmount}</div>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredData.map((row) => (
                            <TableRow key={row.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                <TableCell component="th" scope="row">
                                    <div>{row.title}</div>
                                    <div style={subdetailsStyle}>{ISODateTimeToReadable(row.dateTime)} | {row.createdByName}</div>
                                </TableCell>
                                <TableCell align="right">{row.amount}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
};
