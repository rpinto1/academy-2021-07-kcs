import { configureStore } from '@reduxjs/toolkit'

import countriesReducer from './countriesReducer'
import portfoliosReducer from './portfoliosReducer'


const store = configureStore({
  reducer: {
    // Define a top-level state field named `todos`, handled by `todosReducer`
    countries: countriesReducer,
    portfolios: portfoliosReducer,

  }
})

export default store