"use client";
import React from "react";
import useGlobalState from "../../state/GlobalState";
import { CallStatus } from "@/types/ApiTypes";
import { ExpenseServices } from "@/services/ServicesIndex";
import { ApiGlobalStateManager } from "@/utils/ServiceStateHelper";
import { StatusMessage } from "@/components/StatusMessage";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody } from "@mui/material";
import { ISODateTimeToReadable } from "@/utils/CommonHelper";

const subdetailsStyle: React.CSSProperties = {
    fontSize: '0.7rem',
    color: 'gray',
};

export const ExpenseView: React.FunctionComponent = () => {
    const { expenseStateList, setExpenseStateList } = useGlobalState();

    React.useEffect(() => {
        if (expenseStateList?.status !== CallStatus.Success && !(expenseStateList?.data?.length > 0)) {
            ApiGlobalStateManager(ExpenseServices.GetExpenses(), setExpenseStateList);
        }
    }, [])

    return (
        <>
            <StatusMessage
                display={false}
                notStartedMessage="Loading.."
                successMessage=""
                failureMessage="Something went wrong."
                currentStatus={expenseStateList.status}
            />
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 300 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>
                                <div><b>Particular</b></div>
                                <div style={subdetailsStyle}>{Array.isArray(expenseStateList.data) ? expenseStateList.data.length : 0} transactions</div>
                            </TableCell>
                            <TableCell align="right">
                                <div><b>Amount (₹)</b></div>
                                <div style={subdetailsStyle}>Total: {Array.isArray(expenseStateList.data) ? expenseStateList.data.reduce((total, row) => total + row.amount, 0) : 0}</div>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            expenseStateList.status === CallStatus.Success && expenseStateList.data && expenseStateList.data.length > 0 && (
                                expenseStateList.data.map((row) => (
                                    <TableRow
                                        key={row.id}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                    >
                                        <TableCell component="th" scope="row">
                                            <div>{row.title}</div>
                                            <div style={subdetailsStyle}>{ISODateTimeToReadable(row.dateTime)} | {row.createdByName}</div>
                                        </TableCell>
                                        <TableCell align="right">{row.amount}</TableCell>
                                    </TableRow>
                                ))
                            )
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
};
