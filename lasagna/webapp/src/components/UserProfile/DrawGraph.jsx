import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function DrawGraph({data, dataKey}) {
    return (
        <div>
            <LineChart
                width={700}
                height={500}
                data={data}
                margin={{
                    top: 100,
                    right: 30,
                    left: 20,
                    bottom: 5,
                }}>
                <Line type="monotone" dataKey={dataKey} stroke="#001ff8" /> 
                <CartesianGrid stroke="#ccc" />
                <XAxis dataKey="year" />
                <YAxis />
                <Tooltip />
                <Legend />
            </LineChart>
        </div>
    )
}
