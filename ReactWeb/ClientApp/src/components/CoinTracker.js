
    //import React, { useEffect } from 'react';
    //import * as signalR from "@microsoft/signalr";

    //export const CoinTracker = () => {
    //    useEffect(() => {
    //        const connection = new signalR.HubConnectionBuilder()
    //            .withUrl("/CoinHub")
    //            .build();

    //        connection.on("ReceiveCoinData", function (conId, coinData) {
    //            var coinDataTableBody = document.querySelector("#coinDataTable tbody");
    //            coinDataTableBody.innerHTML = '';

    //            coinData.forEach(function (coin) {
    //                var row = document.createElement("tr");

    //                var symbolCell = document.createElement("td");
    //                symbolCell.textContent = coin.symbol;
    //                row.appendChild(symbolCell);

    //                var nameCell = document.createElement("td");
    //                nameCell.textContent = coin.name;
    //                row.appendChild(nameCell);

    //                var priceCell = document.createElement("td");
    //                priceCell.textContent = coin.price;
    //                row.appendChild(priceCell);

    //                var dateCell = document.createElement("td");
    //                dateCell.textContent = coin.updateTime;
    //                row.appendChild(dateCell);

    //                coinDataTableBody.appendChild(row);
    //            });
    //        });

    //        connection.start()
    //            .then(function () {
    //                console.log("SignalR connection established.");
    //                connection.invoke("SendCoinData").then(function () {
    //                    console.log("Coin data sent.");
    //                });
    //            })
    //            .catch(function (err) {
    //                console.error(err.toString());
    //            });
    //    }, []);

    //    return (
    //        <div>
    //            <h1>Welcome to Coin Tracker</h1>

    //            <table id="coinDataTable" className="table">
    //                <thead>
    //                    <tr>
    //                        <th>Symbol</th>
    //                        <th>Name</th>
    //                        <th>Price</th>
    //                        <th>UpdateTime</th>
    //                    </tr>
    //                </thead>
    //                <tbody></tbody>
    //            </table>
    //        </div>
    //    );
    //};

    //export default CoinTracker;


//import React, { useEffect, useState } from 'react';


//import * as signalR from "@microsoft/signalr";

//export const CoinTracker = () => {
//    useEffect(() => {
//        const connection = new signalR.HubConnectionBuilder()
//            .withUrl("https://localhost:7004/CoinHub")
//            .build();

//        connection.on("ReceiveCoinData", function (conId, coinData) {
//            const coinDataTableBody = document.querySelector("#coinDataTable tbody");
//            coinDataTableBody.innerHTML = '';

//            coinData.forEach(function (coin) {
//                const row = (
//                    <tr key={coin.symbol}>
//                        {coin.map((item, index) => <td key={index}>{item}</td> ) }
//                    </tr>
//                );

//                coinDataTableBody.appendChild(row);
//            });
//        });
//        connection.start()
//            .then(function () {
//                console.log("SignalR connection established.");
//                try {
//                    console.log("1 buraya geldi");

//                    connection.invoke("SendCoinData")

//                        .then(function () {
//                            console.log("Coin data sent.");
//                        })

//                        .catch(function (err) {
//                            console.error(err.toString());
//                        });
//                    console.log("2 buraya geldi");
//                } catch (error) {
//                    console.error(error.toString());
//                }
//            })
//            .catch(function (err) {
//                console.error(err.toString());
//            });

//        //connection.start()
//        //    .then(function () {
//        //        console.log("SignalR connection established.");
//        //        connection.invoke("SendCoinData").then(function () {
//        //            console.log("Coin data sent.");
//        //        });
//        //    })
//        //    .catch(function (err) {
//        //        console.error(err.toString());
//        //    });
//    }, []);

//    return (
//        <div>
//            <h1>Welcome to Coin Tracker</h1>

//            <table id="coinDataTable" className="table">
//                <thead>
//                    <tr>
//                        <th>Symbol</th>
//                        <th>Name</th>
//                        <th>Price</th>
//                        <th>UpdateTime</th>
//                    </tr>
//                </thead>
//                <tbody></tbody>
//            </table>
//        </div>
//    );
//};

//export default CoinTracker;


//import { HubConnectionBuilder } from "@microsoft/signalr";
//import { useEffect } from 'react';
//import React from 'react';
//import { createRoot } from 'react-dom/client';

//  export const CoinTracker = () => {
//    useEffect(() => {
//        const connection = new HubConnectionBuilder()
//            .withUrl("/CoinHub")
//            .build();

//        connection.on("ReceiveCoinData", (conId, coinData) => {
//            const coinDataTableBody = document.querySelector("#coinDataTable tbody");
//            coinDataTableBody.innerHTML = '';

//            coinData.forEach((coin) => {
//                const row = (
//                    <tr key={coin.symbol}>
//                        {coin.map((item, index) => <td key={index}>{item}</td>)}
//                    </tr>
//                );

//                coinDataTableBody.appendChild(row);
//            });
//        });

//        connection.start()
//            .then(() => {
//                console.log("SignalR connection established.");
//                connection.invoke("SendCoinData").then(() => {
//                    console.log("Coin data sent.");
//                });
//            })
//            .catch((err) => {
//                console.error(err.toString());
//            });
//    }, []);

//    return null;
//};

//export default CoinTracker;







import React, { useState, useEffect } from 'react';
import * as signalR from "@microsoft/signalr";

export const CoinTracker = () => {
    const [coinData, setCoinData] = useState([]);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7004/CoinHub")
            .build();

        connection.on("ReceiveCoinData", function (coinData) {
            setCoinData(coinData);
        });

        connection.start()
            .then(function () {
                console.log("SignalR connection established.");
                try {
                    console.log("1 buraya geldi");
                    if (connection) {
                        connection.invoke("SendCoinData")
                            .then(function () {
                                console.log("Coin data sent.");
                            })
                            .catch(function (err) {
                                console.error(err.toString());
                            });
                        console.log("2 buraya geldi");
                    }
                } catch (error) {
                    console.error(error.toString());
                }
            })
            .catch(function (err) {
                console.error(err.toString());
            });

        // Clean up connection on component unmount
        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, []);

    return (
        <div>
            <h1>Welcome to Coin Tracker</h1>

            <table id="coinDataTable" className="table">
                <thead>
                    <tr>
                        <th>Symbol</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>UpdateTime</th>
                    </tr>
                </thead>
                <tbody>
                    {coinData.map((coin) => (
                        <tr key={coin.symbol}>
                            <td>{coin.symbol}</td>
                            <td>{coin.name}</td>
                            <td>{coin.price}</td>
                            <td>{coin.updateTime}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default CoinTracker;

 
 