"use client";
import * as React from 'react';
import { PageCard } from '../../../components/PageCard';
import { Button, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Stack, TextField, Typography } from '@mui/material';
import { StateData } from '../../../state/GlobalState';
import { ApiComponentStateManager, ApiGlobalStateManager } from '@/utils/ServiceStateHelper';
import { ExpenseServices } from '@/services/ServicesIndex';
import { CreateExpensePayload, ExpenseResult } from '@/types/ExpenseApiTypes';
import { StatusMessage } from '@/components/StatusMessage';
import useGlobalState from '@/state/GlobalState';

const submitButtonStyle: React.CSSProperties = {
    width: 90,
}

export const ExpenseAdd: React.FunctionComponent = () => {
    const { setExpenseStateList } = useGlobalState();
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [isTouched, setIsTouched] = React.useState<boolean>(false);
    const [expenseCreationState, setExpenseCreationState] = React.useState<StateData<ExpenseResult>>({} as StateData<ExpenseResult>);
    const [txnType, setTxnType] = React.useState<boolean>(false);
    const [txnAmount, setTxnAmount] = React.useState<number>(0);
    const [formData, setFormData] = React.useState<CreateExpensePayload>({
        title: '',
        amount: 0,
        dateTime: new Date().toISOString(),
    } as CreateExpensePayload);

    const isValid_Title = (title: string) => {
        return title !== '' && title !== null && title.length >= 3;
    }

    const isValid_Amount = (amount: number) => {
        return !isNaN(amount) && amount > 0;
    }

    const validateForm = () => {
        setIsFormValid(isValid_Title(formData.title) && isValid_Amount(txnAmount));
    }

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        if (!isFormValid) return;

        if (txnType === false) {
            formData.amount = 0 - txnAmount;
        }
        else {
            formData.amount = txnAmount;
        }
        e.preventDefault();
        ApiComponentStateManager(ExpenseServices.CreateExpense(formData), setExpenseCreationState).then(() => {
            ApiGlobalStateManager(ExpenseServices.GetExpenses(), setExpenseStateList);
        });
    }

    return (
        <PageCard>

            <form onSubmit={handleFormSubmit} onChange={validateForm}>
                <Stack
                    direction='column'
                    alignContent='center'
                    justifyContent='center'
                    spacing={2}
                    margin={2}
                    maxWidth={400}
                >
                    <Typography variant="h6" color="textSecondary" gutterBottom>
                        Add an Expense
                    </Typography>
                    <FormControl>
                        <FormLabel id="type-label">Transaction Type</FormLabel>
                        <RadioGroup
                            row
                            aria-labelledby="type-label"
                            name="transaction-type"
                            value={txnType.toString()}
                            onChange={(e) => setTxnType(e.target.value === 'true')}
                        >
                            <FormControlLabel value={false} control={<Radio />} label="Debit" />
                            <FormControlLabel value={true} control={<Radio />} label="Credit" />
                        </RadioGroup>
                    </FormControl>
                    <TextField
                        label="Particular"
                        onChange={(e) => { setFormData({ ...formData, title: e.target.value }); setIsTouched(true); }}
                        error={isTouched && !isValid_Title(formData.title)}
                    />
                    <TextField
                        label="Amount in ₹"
                        onChange={(e) => { setTxnAmount(parseFloat(e.target.value)); setIsTouched(true); }}
                        error={isTouched && !isValid_Amount(txnAmount)}
                    />
                    <TextField
                        label="Date"
                        type="date"
                        slotProps={{
                            inputLabel: {
                                shrink: true,
                            },
                        }}
                        value={formData.dateTime.split('T')[0]}
                        onChange={(e) => setFormData({ ...formData, dateTime: e.target.value })}
                    />
                    <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Submit</Button>
                    <StatusMessage
                        display={false}
                        notStartedMessage="Please fill the form and submit."
                        successMessage="Expense added successfully."
                        failureMessage="Failed to add expense."
                        currentStatus={expenseCreationState.status}
                    />
                </Stack>
            </form>
        </PageCard>
    );
}