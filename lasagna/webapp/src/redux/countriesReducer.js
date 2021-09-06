import { createSlice } from '@reduxjs/toolkit'

const initialState = ["US"]

const countriesReducer = createSlice({
  name: 'countries',
  initialState,
  reducers: {
    countryAdd(state, action) {
      // ✅ This "mutating" code is okay inside of createSlice!
       return [...state, action.payload]
    },
    countryDelete(state, action) {
        return state.filter((m,i) => i != action.payload)
    },
  }
})

export const { countryAdd, countryDelete } = countriesReducer.actions

export default countriesReducer.reducer
