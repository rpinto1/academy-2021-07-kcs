import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function DrawGraph({indicator, dataKey}) {
    return (
        <div>
            <LineChart
                width={700}
                height={500}
                data={indicator}
                margin={{
                    top: 100,
                    right: 30,
                    left: 20,
                    bottom: 5,
                }}>
                <Line type="monotone" dataKey={dataKey} stroke="#001ff8" /> 
                {/* <Line type="monotone" dataKey="ROIC" stroke="#001ff8" />
                <Line type="monotone" dataKey="StickerPrice" stroke="#ea1e28" />
                <Line type="monotone" dataKey="Score" stroke="#6cad0e" />
                <Line type="monotone" dataKey="CashFlow" stroke="#c545ff" /> */}
                <CartesianGrid stroke="#ccc" />
                <XAxis dataKey="year" />
                <YAxis />
                <Tooltip />
                <Legend />
            </LineChart>
        </div>
    )
}
