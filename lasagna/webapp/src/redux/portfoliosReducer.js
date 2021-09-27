import { createSlice } from '@reduxjs/toolkit';
import { userId, headers } from '../components/UserManager';

const initialState = [{ portfolioCompanies: [{ ticker: '' }] }];

const portfoliosReducer = createSlice({
    name: 'portfolios',
    initialState,
    reducers: {
        //add portfolios to current user
        portfolioAdd(state, action) {
            // ✅ This "mutating" code is okay inside of createSlice!
            return [...state, action.payload]
        },

        //delete portfolio from user
        portfolioDelete(state, action) {
            return state.filter((m, i) => i != action.payload)
        },


        //populate portfolios list
        portfolioAddBulk(state, action) {
            return action.payload
        }

    }
})

export const { portfolioAdd, portfolioDelete, portfolioAddBulk } = portfoliosReducer.actions

export default portfoliosReducer.reducer
