import { configureStore } from '@reduxjs/toolkit'

import countriesReducer from './countriesReducer'


const store = configureStore({
  reducer: {
    // Define a top-level state field named `todos`, handled by `todosReducer`
    countries: countriesReducer,

  }
})

export default store