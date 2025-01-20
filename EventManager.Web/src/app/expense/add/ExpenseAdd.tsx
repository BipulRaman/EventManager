"use client";
import * as React from 'react';
import { PageCard } from '../../../components/PageCard';
import { Button, Stack, TextField, Typography } from '@mui/material';
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
    const [formData, setFormData] = React.useState<CreateExpensePayload>({
        title: '',
        amount: 0,
        dateTime: new Date().toISOString().split('T')[0],
    } as CreateExpensePayload);

    const isValid_Title = (title: string) => {
        return title !== '' && title !== null && title.length >= 3;
    }

    const isValid_Amount = (amount: number) => {
        return !isNaN(amount) && amount > 0;
    }

    const validateForm = () => {
        setIsFormValid(isValid_Title(formData.title) && isValid_Amount(formData.amount));
    }


    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
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
                    <TextField
                        label="Particular"
                        onChange={(e) => { setFormData({ ...formData, title: e.target.value }); setIsTouched(true); }}
                        error={isTouched && !isValid_Title(formData.title)}
                    />
                    <TextField
                        label="Amount in ₹"
                        onChange={(e) => { setFormData({ ...formData, amount: parseFloat(e.target.value) }); setIsTouched(true); }}
                        error={isTouched && !isValid_Amount(formData.amount)}
                    />
                    <TextField
                        label="Date"
                        type="date"
                        slotProps={{
                            inputLabel: {
                                shrink: true,
                            },
                        }}
                        value={formData.dateTime}
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