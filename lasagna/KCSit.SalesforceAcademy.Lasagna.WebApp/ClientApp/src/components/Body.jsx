import React from 'react'
import GainLose from './GainLose/GainLose'
import { IISList } from './IIS/IISList'


export default function Body() {
    return (
        <div className='content'>
            <GainLose />
            
            <IISList />
        </div>
    )
}