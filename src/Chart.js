
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

const PatientDistributionChart = () => {
  const [chartData, setChartData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get('https://localhost:44306/api/chartData');
      const data = result.data.map((item) => ({
        name: item.Date,
        VerifiedForTheCoronaVirus: item.ActivePatients,
      }));
      setChartData(data);
    };
    fetchData();
  }, []);

  return (
    <div style={{ display: 'flex', justifyContent: 'center' }}>
      <div>
        <h2 style={{ textAlign: 'center' }}>Patient Distribution Chart</h2>
        <LineChart data={chartData} width={1000} height={400} margin={{ top: 20, right: 40, left: 30 }}>
          <XAxis dataKey="name" />
          <YAxis tickCount={3}/>
          <CartesianGrid strokeDasharray="3 3" />
          <Tooltip />
          <Legend />
          <Line type="monotone" dataKey="VerifiedForTheCoronaVirus" stroke="#8884d8" activeDot={{ r: 8 }} />
        </LineChart>
      </div>
    </div>
  );
};

export default PatientDistributionChart;